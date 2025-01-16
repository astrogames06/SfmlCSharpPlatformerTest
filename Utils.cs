namespace App
{
    class Utils
    {
        public static bool CheckCollisions(float a_x, float a_y, int a_width, float a_height, float b_x, float b_y, float b_width, float b_height)
        {
            return (a_x + a_width > b_x) && (a_x < b_x + b_width) && (a_y + a_height > b_y) && (a_y < b_y + b_height);
        }
        public static bool CheckCollisionsY(float a_y, float a_height, float b_y, float b_height)
        {
            return (a_y + a_height > b_y) && (a_y < b_y + b_height);
        }
        public static bool CheckCollisionsTop(float a_y, float a_height, float b_y)
        {
            return (a_y + a_height > b_y);
        }
        public static bool CheckCollisionsX(float a_x, float a_width, float b_x, float b_width)
        {
            return (a_x + a_width > b_x) && (a_x < b_x + b_width);
        }

        public static bool UnCollideX(float a_x, int a_width, float b_x, float b_width)
        {
            return (a_x - a_width < b_x) && (a_x > b_x - b_width);
        }

        public static bool UnCollide(float a_x, float a_y, int a_width, float a_height, float b_x, float b_y, float b_width, float b_height)
        {
            return (a_x - a_width < b_x) && (a_x > b_x - b_width) && (a_y - a_height < b_y) && (a_y > b_y - b_height);
        }
    }
}