using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    public static class DataExtensionMethods
    {
        public static void LoadData(this INoteEntryStorage store)
        {
            var a = new NoteTake
            {
                Title = "Sprint Planning Meeting",
                Text = "1. Scope 2. Backlog 3. Duration"
            };

            var b = new NoteTake
            {
                Title = "Daily Scrum Stand-up",
                Text = "1. Yesterday 2.Today 3. Impediments"
            };

            var c = new NoteTake
            {
                Title = "Sprint Retrospective",
                Text = "1. Reflection 2. Actions"
            };

            Task.WhenAll(
                store.AddAsync(a),
                store.AddAsync(b),
                store.AddAsync(c))
                .ConfigureAwait(false);
        }
    }
}
