namespace RequestProcessingPipeline
{
    public class FromHundredOneToThousandMiddleware
    {
        private readonly RequestDelegate _next;

        public FromHundredOneToThousandMiddleware(RequestDelegate next)
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
                if (number < 101)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else if (number > 1000)
                {
                    await context.Response.WriteAsync("Your number greater then one thousand");

                }
                else if (number == 1000)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is one thousand");
                }
                else
                {
                    string[] Hundr = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    if (number % 100 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Hundr[number / 100 - 1] + " hundread");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        string? result = string.Empty;
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Hundr[number / 100 - 1] + " hundread " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter4");
            }
        }
    }
}
