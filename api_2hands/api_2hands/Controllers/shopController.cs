using api_2hands.Models;
using api_2hands.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using api_2hands.Util;


namespace api_2hands.Controllers
{
    [EnableCors("*", "*", "*")]
    public class shopController : ApiController
    {
        shop_2handEntities db = new shop_2handEntities();


        #region User
        [HttpGet]
        [Route("api/user/list")]
        public List<user> ListUser()
        {
            return db.users.ToList();
        }
        // Lay danh sach user theo id
        [HttpGet]
        [Route("api/user/listuserbyid")]
        public List<user> ListUserById(int id)
        {
            return db.users.Where(p => p.id == id).ToList();
        }
        // Tim user usereo Id
        [HttpGet]
        [Route("api/user/finduserbyid")]
        public user FindUserById(int id)
        {
            return db.users.SingleOrDefault(p => p.id == id);
        }

        // insert user
        [HttpPost]
        [Route("api/user/insertUser")]
        public bool InsertUser(user us)
        {
            try
            {
                user s = db.users.FirstOrDefault(p => p.email == us.email);
                if (s == null)
                {
                    db.users.Add(us);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Da co email nay");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
                return false;
            }
        }
        // Sua user
        [HttpPut]
        [Route("api/user/updateUser")]
        public bool UpdateUser(user us)
        {
            try
            {
                user s = db.users.FirstOrDefault(p => p.id == us.id);
                s.email = us.email;
                s.fullname = us.fullname;
                s.dob = us.dob;
                s.password = us.password;
                s.address = us.address;
                s.phone = us.phone;
                s.status = us.status;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
                return false;
            }
        }

        // Xoa user
        [HttpDelete]
        [Route("api/user/deleteuser")]
        public bool DeleteUser(int id)
        {
            user s = db.users.FirstOrDefault(p => p.id == id);
            if (s != null)
            {
                db.users.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login(user ac)
        {
            var acc = db.users.Where(p => p.email == ac.email && p.password == ac.password).FirstOrDefault();
            return Ok(acc);
        }
        #endregion

        #region Products

        // List Product
        [HttpGet]
        [Route("api/products/listproduct")]
        public List<product> ListProduct()
        {
            return db.products.ToList();
        }

        // Update Product
        [HttpPut]
        [Route("api/products/updateproductbysearch")]
        public IHttpActionResult UpdateOrder(product pr)
        {
            product check = db.products.Where(p => p.id == pr.id).FirstOrDefault();
            if (check != null)
            {
                check.countSearch = pr.countSearch;
                db.SaveChanges();
                return Ok("ok");
            }
            return Ok("Co loi");
        }

        // Search By ProductName
        [HttpGet]
        [Route("api/products/searchproduct")]
        public IHttpActionResult SearchProduct(string name)
        {
            var check = db.products.Where(p => p.name == name).ToList();
            if (check != null)
            {
                return Ok(check);
            }
            return Ok("Co loi");
        }

        // Search By ProductId
        [HttpGet]
        [Route("api/products/searchproductbyId")]
        public IHttpActionResult SearchProduct(int id)
        {
            var check = db.products.Where(p => p.id == id).ToList();
            if (check != null)
            {
                return Ok(check);
            }
            return Ok("Co loi");
        }
        // List product with hot

        [HttpGet]
        [Route("api/products/listproducthot")]
        public List<product> ListProductByStatus(int status)
        { 
            List<product> ds = db.products.Where(p => p.status == status).ToList();
            return ds;
        }

        // List produt by cate
        [HttpGet]
        [Route("api/products/listproductbycate")]
        public List<product> ListProductByCate(int cate)
        {
            List<product> ds = db.products.Where(p => p.cateId == cate).ToList();
            foreach (product sp in ds)
            {
                sp.category = null;
            }
            return ds;
        }

        //insert product
        [HttpPost]
        [Route("api/products/insertproduct")]
        public IHttpActionResult InsertProduct(product pro)
        {

            try
            {

                var pr = db.products.Where(p => p.name == pro.name).FirstOrDefault();
                if (pr != null)
                {
                    return Ok("Da co san pham nay");
                }
                else
                {
                    db.products.Add(pro);
                    db.SaveChanges();
                    return Ok("ok");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("api/products/TestPostFile")]
        public HttpResponseMessage TestPostFile()
        {
            var httpRequest = HttpContext.Current.Request;
            string base641 = HttpContext.Current.Request.Form["dataForm"]?.ToString();
            var base64EncodedBytes = System.Convert.FromBase64String(base641);
            string planTText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            if (httpRequest.Files.Count > 0)
            {
                foreach (string fileName in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[fileName];
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/" + file.FileName);
                    file.SaveAs(filePath);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // update product
        [HttpPost]
        [Route("api/products/updateproduct")]
        public IHttpActionResult UpdateProduct(product pro)
        {
            product prd = db.products.Where(p => p.id == pro.id).FirstOrDefault();
            if (prd != null)
            {
                prd.name = pro.name;
                prd.originalPrice = pro.originalPrice;
                prd.promotionPrice = pro.promotionPrice;
                prd.image = pro.image;
                prd.createdBy = pro.createdBy;
                prd.createdDate = pro.createdDate;
                prd.cateId = pro.cateId;
                prd.qty = pro.qty;
                prd.des = pro.des;
                prd.status = pro.status;
                prd.soldCount = pro.soldCount;
                db.SaveChanges();
                return Ok("Sua thanh cong");
            }
            else
            {
                return Ok("Sua that bai");
            }
        }

        // Update qty product
        [HttpPut]
        [Route("api/products/updateqtyproduct")]
        public IHttpActionResult UpdateQty(product pr)
        {
            product check = db.products.Where(p => p.id == pr.id).FirstOrDefault();
            if (check != null)
            {
                check.qty = pr.qty;
                db.SaveChanges();
                return Ok("ok");
            }
            return Ok("Co loi");
        }

        // Delete Product
        [HttpDelete]
        [Route("api/products/deleteproduct")]
        public bool DeleteProduct(int id)
        {
            product s = db.products.FirstOrDefault(p => p.id == id);
            if (s != null)
            {
                db.products.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        // Find product
        [HttpGet]
        [Route("api/products/findproductbyid")]
        public product FindProductById(int id)
        {
            return db.products.SingleOrDefault(p => p.id == id);
        }
        #endregion

        #region Categories

        // List cate
        [HttpGet]
        [Route("api/categories/listcate")]
        public List<category> ListCate()
        {
            return db.categories.ToList();
        }

        [HttpGet]
        [Route("api/cate/searchcate")]
        public IHttpActionResult SearchCate(string name)
        {
            var check = db.categories.Where(p => p.name == name).ToList();
            if (check != null)
            {
                return Ok(check);
            }
            return Ok("Co loi");
        }

        // Search By ProductId
        [HttpGet]
        [Route("api/cate/searchcatebyId")]
        public IHttpActionResult SearchCateById(int id)
        {
            var check = db.categories.Where(p => p.id == id).ToList();
            if (check != null)
            {
                return Ok(check);
            }
            return Ok("Co loi");
        }
        // List cate
        //[HttpGet]
        //[Route("api/categories/listallcate")]
        //public List<category> ListAllCate(int offset, int limit)
        //{
        //    return db.categories.OrderBy(p => p.id).Skip(offset).Take(limit).ToList();
        //}

        // insert cate
        [HttpPost]
        [Route("api/cate/insertcate")]
        public IHttpActionResult InsertCate(category cate)
        {

            var s = db.categories.Where(p => p.name == cate.name).FirstOrDefault();
            if (s == null)
            {
                db.categories.Add(cate);
                db.SaveChanges();
            }
            return Ok();
        }

        // update cate
        [HttpPost]
        [Route("api/cate/updatecate")]
        public bool UpdateCate(category cate)
        {
            try
            {
                category ct = db.categories.FirstOrDefault(p => p.id == cate.id);
                ct.name = cate.name;
                ct.status = 1;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
                return false;
            }

        }
        // Delete cate
        [HttpDelete]
        [Route("api/cate/deletecate")]
        public bool DeleteCate(int id)
        {
            category s = db.categories.FirstOrDefault(p => p.id == id);
            if (s != null)
            {
                db.categories.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;

        }


        #endregion

        #region Cart
        [HttpGet]
        [Route("api/cart/listcart")]
        public List<cart> ListCart()
        {
            return db.carts.ToList();
        }

        // insert cart
        [HttpPost]
        [Route("api/cart/insertcart")]
        public IHttpActionResult InsertCart(cart ca)
        {
            db.carts.Add(ca);
            db.SaveChanges();
            return Ok("ok");
        }
        // show product from cart
        [HttpGet]
        [Route("api/cart/showcartbyuserid")]
        public List<cart> LitProductByCart(int userID)
        {
            List<cart> list = db.carts.Where(p => p.userId == userID).ToList();
            return list;
        }


        // Delete all data cart
        [HttpDelete]
        [Route("api/cart/deleteallcart")]
        public bool DeleteAllCart(int userId)
        {
            
                db.Database.ExecuteSqlCommand("DELETE FROM cart WHERE userId = " + userId);
                db.SaveChanges();
                return true;
            
        }
        // Delete cart
        [HttpDelete]
        [Route("api/cart/deletecart")]
        public bool DeleteCart(int id)
        {
            cart s = db.carts.FirstOrDefault(p => p.id == id);
            if (s != null)
            {
                db.carts.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Order
        // List order
        [HttpGet]
        [Route("api/order/listorderbyid")]
        public List<order> ListOrdertByCart(string status)
        {
            return db.orders.Where(p => p.status == status).ToList();
        }

        // List order
        [HttpGet]
        [Route("api/order/listorderbyuserid")]
        public List<order> ListOrdertByUserID(int userId)
        {
            return db.orders.Where(p => p.userId == userId).ToList();
        }
        // insert order
        [HttpPost]
        [Route("api/order/inserorder")]
        public IHttpActionResult InserOrder(order or)
        {
            var lst = db.orders.Add(or);
            db.SaveChanges();
            return Ok(lst);
        }
      
        // List cart by status
        [HttpGet]
        [Route("api/order/listorderbystatus")]
        public List<order> ListOrderByStatus(int userID)
        {
            return db.orders.Where(p => p.userId == userID).ToList();
        }

        // Update Order
        [HttpPut]
        [Route("api/order/updateorder")]
        public IHttpActionResult UpdateOrder(order or)
        {
            order check = db.orders.Where(p => p.id == or.id).FirstOrDefault();
            if (check != null)
            {
                check.status = or.status;
                db.SaveChanges();
                return Ok("ok");
            }
            return Ok("Co loi");
        }

        // Get list order success
        [HttpGet]
        [Route("api/order/ordersuccess")]
        public List<order> GetOrderSuccess(string status)
        {

            return db.orders.Where(p => p.status == status).ToList();
        }
        // Get list order success
        [HttpGet]
        [Route("api/order/orderid")]
        public List<order> GetOrderById(int id)
        {

            return db.orders.Where(p => p.id == id).ToList();
        }

        // List order status and month
        [HttpGet]
        [Route("api/order/statusandmonth")]
        public List<order> GetSuccessAndMonth(string status, int month)
        {
            

            return db.orders.Where(p => p.status == status && p.receivedDate.Value.Month == month).ToList();
        }
        // Delete Order
        [HttpDelete]
        [Route("api/order/deleteorder")]
        public bool DeleteOrder(int id)
        {
            order s = db.orders.FirstOrDefault(p => p.id == id);
            if (s != null)
            {
                db.orders.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion
        #region Order Details
        // List order details by orderid
        [HttpGet]
        [Route("api/orderdetail/listorderdetail")]
        public List<order_details> FindOrdertById(int orderId)
        {
            return db.order_details.Where(p => p.orderId == orderId).ToList();
        }

        // Delete Order
        [HttpDelete]
        [Route("api/orderdetail/deleteorderdetail")]
        public bool DeleteOrderDetail(int orderId)
        {
            order_details s = db.order_details.FirstOrDefault(p => p.orderId == orderId);
            if (s != null)
            {
                db.order_details.Remove(s);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("api/orderdetail/insertorderdetail")]
        public IHttpActionResult InsertOrderDetail(order_details ord)
        {
            db.order_details.Add(ord);
            db.SaveChanges();
            return Ok("ok");
        }
        #endregion
    }
}

