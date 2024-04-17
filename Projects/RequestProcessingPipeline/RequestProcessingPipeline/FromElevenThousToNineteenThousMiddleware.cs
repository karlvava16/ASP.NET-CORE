﻿namespace RequestProcessingPipeline
{
    public class FromElevenThousToNineteenThousMiddleware
    {
        private readonly RequestDelegate _next;

        public FromElevenThousToNineteenThousMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if ((number / 1000) % 100 < 11 || (number / 1000) % 100 > 19)
                {
                    await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] Numbers = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    // Выдаем окончательный ответ клиенту
                    // Любые числа больше 20, но не кратные 10
                    if ((number / 1000) < 20)
                         // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Numbers[(number / 1000) % 20 - 11]); // от 11 до 19
                        
                    else
                        // Записываем в сессионную переменную number результат для компонента FromTwentyToHundredMiddleware
                        context.Session.SetString("number", Numbers[(number / 1000) % 20 - 11]);

                }
            }
            catch (Exception e)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter6" + "\n\n" + e.Message + "\n\n");
            }
        }
    }
}