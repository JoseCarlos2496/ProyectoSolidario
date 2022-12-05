using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BackendSolidario.Business.Helper {
    public class ErrorHelper {
        public static ResponseObject Response(int statusCode, string message) {
            return new ResponseObject() {
                Type = "C", //Custom
                StatusCode = statusCode,
                Message = message
            };
        }

        public static List<ModelErrors> GetModelStateErrors(ModelStateDictionary model) {
            return model.Select(x => new ModelErrors() { Type = "M", Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).ToList() }).ToList();
        }
    }

    public class ResponseObject {
        public string Type { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ModelErrors {
        public string Type { get; set; }
        public string Key { get; set; }
        public List<string> Messages { get; set; }
    }
}