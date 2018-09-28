using System;

namespace StudentManagement.Services.Utils
{
    public class StudentNumberGenerator
    {
        public string GenerateNumber()
        {
            string result = "";
            Random random = new Random();

            do
            {
                result += random.Next(0, 9).ToString();
            } while (result.Length < 25);

            return result;
        }
    }
}
