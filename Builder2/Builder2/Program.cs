using System;

namespace Builder2
{
    class Program
    {
        //move validation from builder
        static void Main(string[] args)
        {
            ArticleBuilder builder = new ArticleBuilder();
            Article video = builder.WithId("1")
                                .WithDescription("Fluent Builder Pattern — C# ")
                                .WithLink("https://medium.com/@martinstm/fluent-builder-pattern-c-4ac39fafcb0b")
                                .WithAuthor("Tiago Martins")
                                .WithPrivacy("public")
                                .WithText("Article text")
                                .Build();
            video.Upload();
        }

        public class Article
        {
            public string Id { get; private set; }
            public Author Author { get; private set; }
            public string Description { get; private set; }
            public Uri Link { get; private set; }
            public string Privacy { get; private set; }
            public string Text { get; private set; }

            public Article()
            {
            }

            public void Upload()
            {
                //uploading
            }

            public Article(string id, Author author, string description, Uri link, string privacy, string text)
            {
                Id = id;
                Author = author;
                Description = ArticleValidator.ValidateDescription(description);
                Link = link;
                Privacy = privacy;
                Text = ArticleValidator.ValidateText(text);
            }
        }

        public class ArticleBuilder
        {
            private string Id { get; set; }
            private Author Author { get; set; }
            private string Description { get; set; }
            private Uri Link { get; set; }
            private string Privacy { get; set; }
            private string Text { get; set; }

            public Article Build()
            {
                return new Article(Id, Author, Description, Link, Privacy, Text);
            }

            public ArticleBuilder WithId(string id)
            {
                Id = id;
                return this;
            }

            public ArticleBuilder WithAuthor(string authorId)
            {
                var author = Author.FindAuthor(authorId);
                if (author != null)
                    Author = author;
                else
                    throw new Exception("Author Id is wrong");
                return this;
            }

            public ArticleBuilder WithDescription(string description)
            {
                Description = description;
                return this;
            }

            public ArticleBuilder WithLink(string link)
            {
                Uri uri = new Uri(link);
                Link = uri;
                return this;
            }

            public ArticleBuilder WithPrivacy(string privacy)
            {
                Privacy = privacy;
                return this;
            }

            public ArticleBuilder WithText(string text)
            {
                Text = text;
                return this;
            }


        }

        static class ArticleValidator
        {
            public static string ValidateText(string text)
            {
                string modifiedText = HideCurseWords(text);
                if (modifiedText.Length < 50000)
                    return modifiedText;
                else
                    throw new Exception("Text length is exceeding 50000 symbols");
            }

            public static string ValidateDescription(string description)
            {
                string modifiedDescription = HideCurseWords(description);
                if (description.Length <= 1000)
                    return modifiedDescription;
                else
                    throw new Exception("Description length is exceeding 1000 symbols");
            }

            private static string HideCurseWords(string text)
            {
                //going through some dictionaty with curse words and change it to ***
                throw new NotImplementedException();
            }
        }

        public class Author
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public string Id { get; set; }

            public Author(string name, string surName, string id)
            {
                Name = name;
                SurName = surName;
                Id = id;
            }

            public static Author FindAuthor(string id)
            {
                //searching author in db
                var searchResult = true;
                if (searchResult) // we Find!
                    return new Author("Ivan", "Petrov", id);
                else // author dont exist
                    return null;
            }
        }
    }
}
