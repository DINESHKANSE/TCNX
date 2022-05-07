namespace TCNX.Models
{
    public class UserDashboardDetails
    {
        public string WalletAddress { get; set; } = "";
        public string UserID { get; set; } = "TCNXGUEST";
        public decimal MyPackage { get; set; } = 0;
        public decimal MyInvestment { get; set; } = 0;
        public decimal CoinValue { get; set; } = 0;
        public decimal CoinBalance { get; set; } = 0;
        public decimal TrxBalance { get; set; } = 0;
        public string MyRank { get; set; } = "Introducer";
        public int MyDirect { get; set; } = 0;
        public int MyTeam { get; set; } = 0;
        public int MyTeamLeft { get; set; } = 0;
        public int MyTeamRight { get; set; } = 0;
        public decimal TotalIncome { get; set; } = 0;
        public decimal GrowthIncome { get; set; } = 0;
        public decimal SponsorIncome { get; set; } = 0;
        public decimal Levelincome { get; set; } = 0;
        public decimal BiddingIncome { get; set; } = 0;
        public decimal RechargeIncome { get; set; } = 0;
        public decimal RepurchaseIncome { get; set; } = 0;
    }
}
