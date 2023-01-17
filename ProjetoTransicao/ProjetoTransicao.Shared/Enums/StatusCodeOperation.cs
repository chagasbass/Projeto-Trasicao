namespace ProjetoTransicao.Shared.Enums
{
    public class StatusCodeOperation : Enumeration
    {
        public static StatusCodeOperation OK { get; } = new StatusCodeOperation(200, nameof(OK));
        public static StatusCodeOperation Created { get; } = new StatusCodeOperation(201, nameof(Created));
        public static StatusCodeOperation NoContent { get; } = new StatusCodeOperation(204, nameof(NoContent));
        public static StatusCodeOperation Accepted { get; } = new StatusCodeOperation(202, nameof(Accepted));
        public static StatusCodeOperation BadRequest { get; } = new StatusCodeOperation(400, nameof(BadRequest), @"https://tools.ietf.org/html/rfc7231#section-6.5.1");
        public static StatusCodeOperation Unauthorized { get; } = new StatusCodeOperation(401, nameof(Unauthorized), @"https://tools.ietf.org/html/rfc7235#section-3.1");
        public static StatusCodeOperation Forbidden { get; } = new StatusCodeOperation(403, nameof(Forbidden), @"https://tools.ietf.org/html/rfc7231#section-6.5.3");
        public static StatusCodeOperation NotFound { get; } = new StatusCodeOperation(404, nameof(NotFound), @"https://tools.ietf.org/html/rfc7231#section-6.5.4");
        public static StatusCodeOperation NotAcceptable { get; } = new StatusCodeOperation(406, nameof(NotAcceptable), @"https://tools.ietf.org/html/rfc7231#section-6.5.6");
        public static StatusCodeOperation BusinessError { get; } = new StatusCodeOperation(422, nameof(BusinessError), @"https://tools.ietf.org/html/rfc4918#section-11.2");
        public static StatusCodeOperation InternalServerError { get; } = new StatusCodeOperation(500, nameof(InternalServerError), @"https://tools.ietf.org/html/rfc7231#section-6.6.1");

        private static List<StatusCodeOperation> _operations = new List<StatusCodeOperation>
    {
        OK,Created,NoContent,Accepted,BadRequest,Unauthorized,Forbidden,NotFound,NotAcceptable,
        BusinessError,InternalServerError
    };

        public StatusCodeOperation(int id, string name, string text = null) : base(id, name, text) { }

        public StatusCodeOperation(int id, string name) : base(id, name) { }

        public static StatusCodeOperation RetrieveStatusCode(int number) => _operations.FirstOrDefault(x => x.Id == number);
    }
}