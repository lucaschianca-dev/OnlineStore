namespace OnlineStore.Enums;

public enum OrderStatus
{
    PENDING,          
    AWAITING_PAYMENT,  
    PAYMENT_FAILED,    
    APPROVED,         
    PROCESSING,       
    SHIPPED,         
    DELIVERED,        
    CANCELLED,        
    REFUNDED,         
    RETURNED
}
