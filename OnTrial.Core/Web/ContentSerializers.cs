namespace NQAP
{
    public enum ContentSerializers { Json, Xml }
    public static class ContentSerializersExtenions
    {
        /// <summary>
        /// Takes a known serializer type and returns the Mime type associated with it
        /// </summary>
        /// <param name="pSerializer">The serializer</param>
        /// <returns></returns>
        public static string ToMimeString(this ContentSerializers pSerializer)
        {
            switch (pSerializer)
            {
                case ContentSerializers.Json:
                    return "application/json";
                case ContentSerializers.Xml:
                    return "application/xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
