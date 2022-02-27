using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace TCNX.Models.DBModel
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

            
        }

        public  DbSet<COUNTRY_FLAG> COUNTRY_FLAG { get; set; }
        public  DbSet<Singlelegincomechart> Singlelegincomecharts { get; set; }
        public  DbSet<tblcode> tblcode { get; set; }
        public  DbSet<tblgetlink> tblgetlink { get; set; }
        public DbSet<tblincome> tblincome { get; set; }
        public DbSet<tbllast> tbllast { get; set; }
        public DbSet<tbllastpay> tbllastpay { get; set; }
        public DbSet<tbllinkdetail> tbllinkdetails { get; set; }
        public DbSet<tblpackage> tblpackage { get; set; }
        public DbSet<tblpayout> tblpayout { get; set; }
        public DbSet<tblpin> tblpin { get; set; }
        public DbSet<tblpinhistory> tblpinhistory { get; set; }
        public DbSet<tblsetting> tblsetting { get; set; }
        public DbSet<tblUserCryptoDetail> tblUserCryptoDetails { get; set; }
        public DbSet<tblUserSetting> tblUserSetting { get; set; }
        public DbSet<tblCmbDetail> tblCmbDetails { get; set; }
        public DbSet<otphistory> otphistory { get; set; }
        public DbSet<tblcoinlist> tblcoinlist { get; set; }
        public DbSet<tblotphistory> tblotphistory { get; set; }
        public DbSet<tblsliderimg> tblsliderimg { get; set; }
        public DbSet<tblregistration> tblregistration { get; set; }
        public DbSet<nowpayment_invoice_history> nowpayment_invoice_history { get; set; }
        public DbSet<tbltranshistory> tbltranshistory { get; set; }
        public DbSet<nowpayment_history> nowpayment_history { get; set; }
        public DbSet<single_leg_income> single_leg_income { get; set; }

    }
}
