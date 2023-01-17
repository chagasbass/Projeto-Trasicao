namespace ProjetoTransicao.Shared.Notifications;

public interface INotificationServices
{
    public StatusCodeOperation StatusCode { get; set; }

    void AddNotification(Notification notificacao);
    void AddNotification(Notification notificacao, StatusCodeOperation statusCode);
    void AddNotifications(IEnumerable<Notification> notificacoes);
    void AddNotifications(IEnumerable<Notification> notificacoes, StatusCodeOperation statusCode);
    void AddStatusCode(StatusCodeOperation statusCode);
    IEnumerable<Notification> GetNotifications();
    bool HasNotifications();
    void ClearNotifications();
}