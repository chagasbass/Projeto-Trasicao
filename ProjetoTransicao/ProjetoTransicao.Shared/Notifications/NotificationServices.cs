namespace ProjetoTransicao.Shared.Notifications;

public class NotificationServices : INotificationServices
{
    public StatusCodeOperation StatusCode { get; set; }

    private readonly ICollection<Notification> _notificacoes;

    public NotificationServices()
    {
        _notificacoes = new List<Notification>();
    }

    public void AddNotification(Notification notificacao) => _notificacoes.Add(notificacao);

    public void AddNotification(Notification notificacao, StatusCodeOperation statusCode)
    {
        AddNotification(notificacao);
        AddStatusCode(statusCode);
    }

    public void AddNotifications(IEnumerable<Notification> notificacoes)
    {
        foreach (var notificacao in notificacoes)
        {
            _notificacoes.Add(notificacao);
        }
    }

    public void AddNotifications(IEnumerable<Notification> notificacoes, StatusCodeOperation statusCode)
    {
        AddNotifications(notificacoes);
        AddStatusCode(statusCode);
    }

    public void AddStatusCode(StatusCodeOperation statusCode) => StatusCode = statusCode;

    public bool HasNotifications() => GetNotifications().Any();

    public IEnumerable<Notification> GetNotifications() => _notificacoes;

    public void ClearNotifications()
    {
        _notificacoes.Clear();
    }
}