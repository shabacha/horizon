using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace horizon.Models
{
        public class Product : Crud
        {
            private int id;
            private string reference;
            private string name;
            private string desc;
            private float price;

            public int Id { get => id; set => id = value; }
            public string Reference { get => reference; set => reference = value; }
            public string Name { get => name; set => name = value; }
            public string Desc { get => desc; set => desc = value; }
            public float Price { get => price; set => price = value; }


            public IList<Product> List()
            {

                List<Product> Listpdt = new List<Product>();
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("Listproducts", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            Product product = new Product();
                            product.Id = Convert.ToInt32(read["id"]);
                            product.Name = read["name"].ToString();
                            product.Desc = read["description"].ToString();
                            product.Price = Convert.ToInt32(read["price"]);
                            product.Reference = read["ref"].ToString();
                            Listpdt.Add(product);

                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                    }
                }

                return Listpdt;
            }
        
            public override void Add()
            {
                try
                {
                    SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                    SqlCommand cmd = new SqlCommand("addproduct", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ref", this.Reference.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@name", this.Name.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@description", this.Desc.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@price", this.Price);
                    connect.Open();
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }

            }
            public override void Affiche(int id)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                    {
                        SqlCommand cmd = new SqlCommand("selectproductbyid", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {

                            this.Id = Convert.ToInt32(read["id"]);
                            this.Reference = read["ref"].ToString();
                            this.Name = read["name"].ToString();
                            this.Desc = read["description"].ToString();
                            this.Price = Convert.ToInt32(read["price"]);
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }

            }


            public override string Delete(int id)
            {
                string msg = "";
                try
                {
                    using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                    {
                        SqlCommand cmd = new SqlCommand("Deleteproduct", connect);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        connect.Open();
                        int rowAffected = cmd.ExecuteNonQuery();
                        connect.Close();
                        msg = "Produit supprimé";

                    }
                }
                catch (Exception ex)
                {
                    msg = "Produit ne peut pas être supprimé";
                }

                return msg;
            }
            public override void Update()
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                    {
                        SqlCommand cmd = new SqlCommand("updateproduct", connect);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", this.Id);
                        cmd.Parameters.AddWithValue("@ref", this.Reference.isNull(string.Empty));
                        cmd.Parameters.AddWithValue("@name", this.Name.isNull(string.Empty));
                        cmd.Parameters.AddWithValue("@Description", this.Desc.isNull(string.Empty));
                        cmd.Parameters.AddWithValue("@price", this.Price);
                        connect.Open();
                        int rowAffected = cmd.ExecuteNonQuery();
                        connect.Close();
                    }
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }

            }
        }
    }
