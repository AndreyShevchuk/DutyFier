namespace DutyFier.Core.Entities
{
    public class Executor
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int DutyId { get; set; }
        public virtual Duty Duty { get; set; }
        //TODO (Done) Deleted Score!

    }
}
