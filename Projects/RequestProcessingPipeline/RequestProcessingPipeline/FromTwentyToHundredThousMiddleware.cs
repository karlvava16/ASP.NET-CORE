namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredThousMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredThousMiddleware(RequestDelegate next)
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
                if (number < 20000)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else if ((number / 1000) > 100)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Number greater than one hundred thousand");
                }
                else if (number == 100000)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is one hundred thousand");
                }
                else
                {
                    string[] Tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    if ((number / 1000) % 10 == 0 && (number / 1000) < 100)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Tens[(number / 1000) / 10 - 2]); 
                    }
                    else
                    { 
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        string? result = string.Empty;
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                        // Выдаем окончательный ответ клиенту
                        //await context.Response.WriteAsync("Your number is " + Tens[number / 10 - 2] + " " + result);

                        if ((number / 1000) > 119)
                            // Записываем в сессионную переменную number результат для компонента FromTwentyToHundredMiddleware
                            context.Session.SetString("number", Tens[((number / 1000) % 100) / 10 - 2] + " " + result);
                        else if ((number / 1000) > 100)
                            context.Session.SetString("number", ""+result);
                        else
                            // Выдаем окончательный ответ клиенту
                            await context.Response.WriteAsync("Your number is " + Tens[(number / 1000) / 10 - 2] + " " + result);

                    }
                }              
            }
            catch (Exception e)
            {
                //"Incorrect parameter"
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter7"  + "\n\n" + e.Message + "\n\n");
            }
        }
    }
}
