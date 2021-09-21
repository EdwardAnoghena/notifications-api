using NotificationsApi.V1.Common.Enums;
using System;

namespace NotificationsApi.V1.Boundary.Response
{
    public class NotificationDetailsObject
    {
        public string PaymentInfoAddress { get; set; }
        public string TransferInfoAddress { get; set; }
        public string Payee { get; set; }
        public string PaymentReference { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ArrearsAfterPayment { get; set; }
        public decimal CurrentArrears { get; set; }
        public string RentAccountNumber { get; set; }
        public string Resident { get; set; }
        public string RequestedBy { get; set; }
        public string Officer { get; set; }
        public DateTime Date { get; set; }
    }
}
