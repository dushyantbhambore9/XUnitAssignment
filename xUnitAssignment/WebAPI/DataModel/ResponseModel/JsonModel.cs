namespace WebAPI.DataModel.ResponseModel
{
    public class JsonModel
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
        public JsonModel(int statusCode, string message, object data)
        {
            this.StatusCode = statusCode;
            this.message = message;
            this.Data = data;
        }
    }
}
