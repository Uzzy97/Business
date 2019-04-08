using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Data
{
    public class FileEntryStorage : INoteEntryStorage
    {
        List<NoteTake> loadedNotes;
        string filename;

        public FileEntryStorage()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            if (string.IsNullOrEmpty(folder))
                folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            this.filename = Path.Combine(folder, "meetings.xml");
        }
        private async Task InitializeAsync()
        {
            if (loadedNotes == null)
            {
                loadedNotes = (await ReadDataAsync(filename)).ToList();
            }
        }



        private async Task AddAsync(NoteTake entry)
        {
            await InitializeAsync();

            if (!loadedNotes.Remove(entry))
            {
                await SaveDataAsync(filename, loadedNotes);
            }
        }

        public async Task DeleteAsync(NoteTake entry)
        {
            await InitializeAsync();
            if (loadedNotes.Remove(entry))
            {
                await SaveDataAsync(filename, loadedNotes);
            }
        }

        public async Task<IEnumerable<NoteTake>> GetAllAsync()
        {
            await InitializeAsync();
            return loadedNotes.OrderByDescending(n => n.CreatedDate);
        }
        public async Task<NoteTake> GetByIDAsync(string id)
        {
            await InitializeAsync();
            return loadedNotes.SingleOrDefault(n => n.ID == id);
        }
        public async Task UpdateAsync(NoteTake entry)
        {
            await InitializeAsync();
            if (!loadedNotes.Contains(entry))
            {
                throw new Exception($"NoteTake {entry.Title} was not found in the {nameof(FileEntryStorage)}. Did you forget to add it?");
            }
            await SaveDataAsync(filename, loadedNotes);
        }

        private static async Task<IEnumerable<NoteTake>> ReadDataAsync(string filename)
        {
            if (!File.Exists(filename))
            {
                return Enumerable.Empty<NoteTake>();
            }

            string text;
            using (var reader = new StreamReader(filename))
            {
                text = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return Enumerable.Empty<NoteTake>();
            }
            IEnumerable<NoteTake> result = XDocument.Parse(text)
                .Root
                .Elements("entry")
                .Select(e =>
                new NoteTake
                {
                    Title = e.Attribute("title").Value,
                    Text = e.Attribute("text").Value,
                    CreatedDate = (DateTime)e.Attribute("createdDate")
                });
            return result;
        }

        static async Task SaveDataAsync(string filename, IEnumerable<NoteTake> notes)
        {
            XDocument root = new XDocument(
                new XElement("minutes",
                notes.Select(n =>
                new XElement("entry",
                new XAttribute("title", n.Title ?? ""),
                new XAttribute("text", n.Text ?? ""),
                new XAttribute("createdDate", n.CreatedDate)))));

            using (StreamWriter writer = new StreamWriter(filename))
            {
                await writer.WriteAsync(root.ToString()).ConfigureAwait(false);

            }
        }
    }
}