namespace OrchestratorService.DTO;

    public class TransferDTO
    {
        public Int32 accountReleasedFk {get; set;}
        public Int32 acccountReceivedFk {get; set;}
        public Decimal transferAmount {get; set;}
    }