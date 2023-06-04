namespace TableStore.Models
{
    public class Cart
    {
        private List<Position> selections = new List<Position>();

        public IEnumerable<Position> Selections { get => selections; }

        public Cart AddItem(Table t, int count, int available)
        {
            if (available != 0)
            {
				Position position = selections.Where(e => e.TableId == t.Id).FirstOrDefault();
				if (position != null)
				{
					if (position.Count < available)
					{
						position.Count += count;
					}
				}
				else
				{
					selections.Add(new Position
					{
						TableId = t.Id,
						Table = t,
						Count = count,
						SellingPrice = t.Price
					});
				}
			}
            return this;
        }
        public Cart RemoveItem(int tableId)
        {
            selections.RemoveAll(e => e.TableId == tableId);
            return this;
        }
        public void Clear() => selections.Clear();
    }
}
