using System.Text;

namespace DecodePyramid
{
    public static class DecodeUtils
    {
        public async static Task<string> decode(string message_file)
        {
            string fileContent = await File.ReadAllTextAsync(message_file);
            string[] lines = fileContent.Split(Environment.NewLine);

            List<string> linesOrdered = lines
                .OrderBy(l => l.Split(' ')[0])
                .Where(l => l != "") // Remove empty lines
                .ToList();

            int increment = 1;
            StringBuilder sb = new();
            for (int i = 0; i < linesOrdered.Count; i+=increment)
            {
                sb.Append(linesOrdered[i].Split(' ')[1]);
                sb.Append(' ');
                increment++;
            }

            return sb.ToString().Trim();
        }
    }
}
