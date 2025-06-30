namespace WebAPI.DataModel.ResponseModel
{
    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }


    }
}
