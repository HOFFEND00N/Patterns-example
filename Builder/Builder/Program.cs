using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            HtmlBuilder htmlBuilder = new HtmlBuilder();
            director.MakeDocument(htmlBuilder);
            Console.WriteLine(htmlBuilder.GetResult());

            MarkdownBuilder markdownBuilder = new MarkdownBuilder();
            director.MakeDocument(markdownBuilder);
            Console.WriteLine(markdownBuilder.GetResult());
        }

        class Director
        {
            public string MakeDocument(IDocumentBuilder builder)
            {
                builder.SetTierOneHeader("Tier 1 header");
                builder.SetTierTwoHeader("Tier 2 header");
                builder.SetText("plain text");
                return builder.GetResult();
            }
        }

        interface IDocumentBuilder
        {
            public void SetText(string text);
            public void SetTierOneHeader(string header);
            public void SetTierTwoHeader(string header);
            public string GetResult();
        }

        class HtmlBuilder : IDocumentBuilder
        {
            private string text, h1, h2;

            public void SetTierOneHeader(string header)
            {
                h1 = "<h1>" + header + "</h1>" + "\n";
            }

            public void SetTierTwoHeader(string header)
            {
                h2 = "<h2>" + header + "</h2>" + "\n";
            }

            public void SetText(string text)
            {
                this.text = "<p>" + text + "</p>" + "\n";
            }

            public string GetResult()
            {
                return h1 + h2 + text;
            }
        }

        class MarkdownBuilder : IDocumentBuilder
        {
            private string text, h1, h2;

            public void SetTierOneHeader(string header)
            {
                h1 = "# " + header + "\n";
            }

            public void SetTierTwoHeader(string header)
            {
                h2 = "## " + header + "\n";
            }

            public void SetText(string text)
            {
                this.text = text + "\n";
            }

            public string GetResult()
            {
                return h1 + h2 + text;
            }
        }
    }
}
