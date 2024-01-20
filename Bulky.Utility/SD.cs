using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public static class SD
    {
		#region User-Roles

		public const string ROLE_ADMIN = "Admin";
        public const string ROLE_COMPANY = "Company";
        public const string ROLE_CUSTOMER = "Customer";
        public const string ROLE_EMPLOYEE = "Employee";

		#endregion

		#region Order-Statuses

		public const string ORDER_STATUS_PENDING = "Pending";
		public const string ORDER_STATUS_SHIPPED = "Shipped";
		public const string ORDER_STATUS_APPROVED = "Approved";
		public const string ORDER_STATUS_REFUNDED = "Refunded";
		public const string ORDER_STATUS_CANCELLED = "Cancelled";
		public const string ORDER_STATUS_PROCESSING = "Processing";
		
		#endregion

		#region Payment-Statuses

		public const string PAYMENT_STATUS_PENDING = "Pending";
		public const string PAYMENT_STATUS_APPROVED = "Approved";
		public const string PAYMENT_STATUS_REJECTED = "Rejected";
		public const string PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT = "ApprovedForDelayedPayment";

        #endregion

        #region Session-keys

        public const string SESSION_SHOPPING_CART = "SessionShoppingCart";

        #endregion
    }
}
