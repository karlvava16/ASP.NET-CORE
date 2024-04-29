using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Validation.StudentsDb.Annotations
{
    public class MyAuthorsAttribute : ValidationAttribute
    {
        //массив для хранения допустимых авторов
        private static string[] myAuthors;

        public MyAuthorsAttribute(string[] Authors)
        {
            myAuthors = Authors;
        }

        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                string? strval = value.ToString();
                for (int i = 0; i < myAuthors.Length; i++)
                {
                    if (strval == myAuthors[i])
                        return true;
                }
            }
            return false;
        }
    }
}