namespace TableStore.Models
{
    public class Table
    {
        int id;
        int consignmentId;
        int count;
        string manufacturer;
        string model;
        int lifeTime;
        int guarantee;
        int weight;
        int width;
        int height;
        int depth;
        string countertopMaterial;
        string underframeMaterial;
        string countertopColor;
        string underframeColor;
        string countertopType;
        int maxLoad;
        string type;
        int price;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int ConsignmentId
        {
            get
            {
                return consignmentId;
            }
            set
            {
                consignmentId = value;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
            }
        }
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        public int LifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                lifeTime = value;
            }
        }
        public int Guarantee
        {
            get
            {
                return guarantee;
            }
            set
            {
                guarantee = value;
            }
        }
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }
        public string CountertopMaterial
        {
            get
            {
                return countertopMaterial;
            }
            set
            {
                countertopMaterial = value;
            }
        }
        public string UnderframeMaterial
        {
            get
            {
                return underframeMaterial;
            }
            set
            {
                underframeMaterial = value;
            }
        }
        public string CountertopColor
        {
            get
            {
                return countertopColor;
            }
            set
            {
                countertopColor = value;
            }
        }
        public string UnderframeColor
        {
            get
            {
                return underframeColor;
            }
            set
            {
                underframeColor = value;
            }
        }
        public string CountertopType
        {
            get
            {
                return countertopType;
            }
            set
            {
                countertopType = value;
            }
        }
        public int MaxLoad
        {
            get
            {
                return maxLoad;
            }
            set
            {
                maxLoad = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public Consignment Consignment { get; set; }
    }
}
