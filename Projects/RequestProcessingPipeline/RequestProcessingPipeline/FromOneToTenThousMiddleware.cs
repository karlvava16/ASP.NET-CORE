namespace RequestProcessingPipeline
{
    public class FromOneToTenThousMiddleware
    {
        private readonly RequestDelegate _next;

        public FromOneToTenThousMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if(number == 10)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is ten");
                }
                else
                {
                    string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                    // Любые числа больше 20, но не кратные 10
                    if ((number / 1000) > 20)
                        // Записываем в сессионную переменную number результат для компонента FromTwentyToHundredMiddleware
                        context.Session.SetString("number", Ones[(number / 1000) % 10 - 1]); 
                    else
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Ones[(number / 1000) - 1]); // от 1 до 9
                }            
            }
            catch(Exception e)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter5" + "\n\n" + e.Message + "\n\n");
            }
        }
    }
}
