namespace Application.DTOs
{
    /// <summary>
    /// 支付完成事件（發給背景消費者處理後續步驟）
    /// </summary>
    public record PaymentCompletedEvent
    (
        string RecordCode,
        int OrderId,
        int PaymentId,
        string OccurredAtUtc
    );
}
