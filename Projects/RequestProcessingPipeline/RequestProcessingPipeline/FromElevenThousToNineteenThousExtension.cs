namespace RequestProcessingPipeline
{
    public static class FromElevenThousToNineteenThousExtension
    {
        public static IApplicationBuilder UseFromElevenThousToNineteenThous(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenThousToNineteenThousMiddleware>();
        }
    }
}
