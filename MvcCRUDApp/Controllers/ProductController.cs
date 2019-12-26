using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcCRUDApp.Models;

namespace MvcCRUDApp.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source = LOVE-TW\SQLEXPRESS; Initial Catalog = MvcCRUDDB; Integrated Security = False; User Id=sa; Password=full321; MultipleActiveResultSets=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from Product";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
            }
            return View(dt);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection (connectionString))
            {
                con.Open();
                string query = "Insert Into Product Values (@ProductName,@Price,@Quantity)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Quantity", productModel.Quantity);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from Product where ProductID = @ProductID";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sda.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dt.Rows[0][0].ToString());
                productModel.ProductName = dt.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                productModel.Quantity = Convert.ToInt32(dt.Rows[0][3].ToString());
                return View(productModel);
            }
            else
            return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Update Product Set ProductName = @ProductName,Price = @Price, Quantity = @Quantity where ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Quantity", productModel.Quantity);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from Product where ProductID = @ProductID";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sda.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dt.Rows[0][0].ToString());
                productModel.ProductName = dt.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                productModel.Quantity = Convert.ToInt32(dt.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Delete from Product where ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}