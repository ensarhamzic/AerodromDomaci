using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;

namespace AerodromDomaci
{
    public class DB
    {
        public string connectionString;
        public MySqlConnection con;
        private MySqlCommand cmd;

        public DB()
        {
            connectionString = @"server=localhost;userid=admin;password=root;database=aerodrom";
            con = new MySqlConnection(connectionString);
            cmd = new MySqlCommand();
        }

        public bool Create(Avion avion)
        {
            try
            {
                int vlasnikId;
                int tip = 1;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"Select id from vlasnik where ime='{avion.Vlasnik.Ime}' and prezime='{avion.Vlasnik.Prezime}'";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   vlasnikId = reader.GetInt32(0);
                }
                else
                {
                    reader.Close();
                    cmd.CommandText = $"insert into vlasnik (ime, prezime) values ('{avion.Vlasnik.Ime}', '{avion.Vlasnik.Prezime}')";
                    cmd.ExecuteNonQuery();
                    vlasnikId = (int)cmd.LastInsertedId;
                }
                reader.Close();

                cmd.CommandText = $"Select id from tip where ime_tipa='{avion.Tip}'";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tip = reader.GetInt32(0);
                }
                reader.Close();

                cmd.CommandText = $"INSERT INTO avion (tip, ser_br, reg_br,vlasnik, br_sedista, kapacitet_rez,nosivost) values ({tip}, {avion.SerijskiBroj}, {avion.RegistracioniBroj}, {vlasnikId}, {avion.BrojSedista}, {avion.KapacitetRezervoara}, {avion.Nosivost})";
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public object Read()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                cmd.CommandText = "SELECT * FROM avion left join ratni_info on avion.id=ratni_info.avion_id inner join vlasnik on avion.vlasnik=vlasnik.id inner join tip on avion.tip=tip.id";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
    }
}
