using System;
using System.Linq;

namespace TaskPlanner
{
    public class RequestHandler
    {
        private MemoryDBProvider dbProvider;

        public RequestHandler()
        {
            dbProvider = new MemoryDBProvider();
        }

        public ReplyLoadFiltered AufgabeLaden(RequestLoadFiltered request)
        {
            string[] tags = ParseQuery(request.Filter);
            var aufgaben = dbProvider.AufgabenLaden(tags);
            var aufgabenInfos = Map(aufgaben);
            return new ReplyLoadFiltered
            {
                Filter = request.Filter,
                AufgabeInfos = aufgabenInfos
            };
        }

        private AufgabeInfo[] Map(Aufgabe[] aufgaben)
        {
            return aufgaben.Select(aufgabe => new AufgabeInfo
            {
                Text = aufgabe.Text,
                Done = aufgabe.Done
            }).ToArray();
        }

        private string[] ParseQuery(string filter)
        {
            var tags = filter.Split(new[] {'#', ' '}, StringSplitOptions.RemoveEmptyEntries);
            return tags.Select(t => '#' + t).ToArray();
        }

        public ReplayLoadTags TagsLaden(RequestLoadTags request)
        {
            var tags = dbProvider.TagsLaden();
            return new ReplayLoadTags {Tags = tags.Select(t => new Tag {Text=t}).ToArray()};
        }
    }

}