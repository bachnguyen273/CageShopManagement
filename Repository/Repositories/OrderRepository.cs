using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int orderId) => OrderDAO.Instance.Remove(orderId);

        public Order GetOrderById(int orderId) => OrderDAO.Instance.GetOrderById(orderId);

        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrderList();

        public void InsertOrder(Order order) => OrderDAO.Instance.AddNew(order);

        public void UpdateOrder(Order order) => OrderDAO.Instance.Update(order);
    }
}
