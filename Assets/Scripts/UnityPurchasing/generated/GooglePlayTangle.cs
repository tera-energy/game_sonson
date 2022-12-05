// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ANvdYxwGggelRGpcuqqfbWLiLtOh23T2pG3MwiU8lO21/Nj5BQ94+BvV+rifxNWhKyV/ffcFpLQuq65kfLY7IIafi5ZK8i2Nf9patPX7uqqD0WuJiVHOQPgRF9txi5+a5B1Gp3M8MaaPfiixz3TkWObyud9cGR1dXVuB6FL2Bi6f1+wXGq0PhspustwmpauklCalrqYmpaWkI3Ny8lr7TBb2gUG3KMQmyzTP0Xi+lF78NE6IlCalhpSpoq2OIuwiU6mlpaWhpKe4uS2e5v9y9jlgaKAYIIy1zatTb9J6PzorarsMFkXh2UHqtbSYxcQ/TFXlzQxkruzG6/iOermo99cCIm2Xo6qcQ8VBQQ9XGHCGd93oilJCyRCqxeA2vo3826anpaSl");
        private static int[] order = new int[] { 2,10,5,4,6,6,9,10,12,9,10,11,12,13,14 };
        private static int key = 164;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
