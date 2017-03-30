using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace AXSH2
{
    class Centroid
    {
        /**
	 * 求三角形质心算法
	 *
	 * @param  r1 Round对象
	 * @param  r2
	 * @param  r3
	 * @return  Coordinate对象
	 */
        public static Coordinate triCentroid(Round r1, Round r2, Round r3) {
        /*有效交叉点1*/
		Coordinate p1 = null;
		/*有效交叉点2*/
		Coordinate p2 = null;
		/*有效交叉点3*/
		Coordinate p3 = null;
		
		/*三点质心坐标*/
		Coordinate centroid = new Coordinate();
		
		/*r1,r2交点*/
		List<Coordinate> intersections1 = intersection(r1.getX(), r1.getY(), r1.getR(), 
											r2.getX(), r2.getY(), r2.getR());
		
		if (intersections1 != null && intersections1.Count!=0) {
			foreach (Coordinate i in intersections1) {
				if (p1==null&&Math.Pow(i.getX()-r3.getX(),2) 
						+ Math.Pow(i.getY()-r3.getY(),2) <= Math.Pow(r3.getR(),2)) {
					p1 = i;
				}else if(p1!=null){
					if(Math.Pow(i.getX()-r3.getX(),2) + Math.Pow(i.getY()
							-r3.getY(),2)<= Math.Pow(r3.getR(),2)){
						if(Math.Sqrt(Math.Pow(i.getX()-r3.getX(),2) 
								+ Math.Pow(i.getY()-r3.getY(),2))>Math.Sqrt(Math.Pow(p1.getX()
										-r3.getX(),2) + Math.Pow(p1.getY()-r3.getY(),2))){
							p1 = i;	
						}
					}
				}
			}
		} else {//没有交点定位错误
            Console.WriteLine("-----------R1和R2交点为空-----------------");
			return null;
		}
		
		/*r1,r3交点*/
		List<Coordinate> intersections2 = intersection(r1.getX(), r1.getY(), r1.getR(), 
											r3.getX(), r3.getY(), r3.getR());
		
		if (intersections2 != null && intersections2.Count!=0) {
			foreach (Coordinate i in intersections2) {//有交点
				if (p2==null&&Math.Pow(i.getX()-r2.getX(),2) 
						+ Math.Pow(i.getY()-r2.getY(),2) <= Math.Pow(r2.getR(),2)) {
					p2 = i;

				}else if(p2!=null){
					if(Math.Pow(i.getX()-r2.getX(),2) + Math.Pow(i.getY()
							-r2.getY(),2) <= Math.Pow(r2.getR(),2)){
						if(Math.Pow(i.getX()-r2.getX(),2) + Math.Pow(i.getY()
								-r2.getY(),2)>Math.Sqrt(Math.Pow(p2.getX()-r2.getX(),2) 
										+ Math.Pow(p2.getY()-r2.getY(),2))){
							p1 = i;	
						}
					}
				}
			}
		} else {//没有交点定位错误
            Console.WriteLine("-----------R1和R3交点为空-----------------");
			return null;
		}
		
		/*r3,r2交点*/
		List<Coordinate> intersections3 = intersection(r2.getX(), r2.getY(), r2.getR(),
											r3.getX(), r3.getY(), r3.getR());
		
		if (intersections3 != null && intersections3.Count!=0) {
			foreach (Coordinate i in intersections3) {//有交点
				if (Math.Pow(i.getX()-r1.getX(),2) 
						+ Math.Pow(i.getY()-r1.getY(),2) <= Math.Pow(r1.getR(),2)) {
					p3 = i;
				}else if(p3!=null){
					if(Math.Pow(i.getX()-r1.getX(),2) + Math.Pow(i.getY()
							-r1.getY(),2) <= Math.Pow(r1.getR(),2)){
						if(Math.Pow(i.getX()-r1.getX(),2) + Math.Pow(i.getY()
								-r1.getY(),2)>Math.Sqrt(Math.Pow(p3.getX()-r1.getX(),2)
										+ Math.Pow(p3.getY()-r1.getY(),2))){
							p3 = i;	
						}
					}
				}
			}
		} else {//没有交点定位错误
            Console.WriteLine("-----------R2和R3交点为空-----------------");
			return null;
		}
		
		/*质心*/
		centroid.setX((p1.getX()+p2.getX()+p3.getX())/3);
		centroid.setY((p1.getY()+p2.getY()+p3.getY())/3);
		
		return centroid;
	}

        /**
         * 求两个圆的交点
         *
         * @param  x1  圆心1横坐标
         * @param  y1  圆心1纵坐标
         * @param  r1  圆心1半径
         * @param  x2  圆心2横坐标
         * @param  y2  圆心2纵坐标
         * @param  r2 圆心2半径
         * @return 返回两个圆的交点坐标对象列表
         */
        public static List<Coordinate> intersection(double x1, double y1, double r1,
                                                    double x2, double y2, double r2)
        {

            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));// 两圆心距离

           // if (Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) < (r1 + r2))
           // {// 两圆相交

            //}

            List<Coordinate> points = new List<Coordinate>();//交点坐标列表

            Coordinate coor;

            if (d > r1 + r2 || d < Math.Abs(r1 - r2))
            {//相离或内含
                return null;
            }
            else if (x1 == x2 && y1 == y2)
            {//同心圆
                return null;
            }
            else if (y1 == y2 && x1 != x2)
            {
                double a = ((r1 * r1 - r2 * r2) - (x1 * x1 - x2 * x2)) / (2 * x2 - 2 * x1);
                if (d == Math.Abs(r1 - r2) || d == r1 + r2)
                {// 只有一个交点时
                    coor = new Coordinate();
                    coor.setY(Math.Abs(a/20));
                    coor.setY(Math.Abs(y1/20));
                    points.Add(coor);
                }
                else
                {// 两个交点
                    double t = r1 * r1 - (a - x1) * (a - x1);
                    coor = new Coordinate();
                    coor.setX(Math.Abs(a/20));
                    coor.setY(Math.Abs((y1 + Math.Sqrt(t))/20));
                    points.Add(coor);
                    coor = new Coordinate();
                    coor.setX(Math.Abs(a/20));
                    coor.setY(Math.Abs(y1 - Math.Sqrt(t))/20);
                    points.Add(coor);
                }
            }
            else if (y1 != y2)
            {
                double k, disp;
                k = (2 * x1 - 2 * x2) / (2 * y2 - 2 * y1);
                disp = ((r1 * r1 - r2 * r2) - (x1 * x1 - x2 * x2) - (y1 * y1 - y2 * y2))
                        / (2 * y2 - 2 * y1);// 直线偏移量
                double a, b, c;
                a = (k * k + 1);
                b = (2 * (disp - y1) * k - 2 * x1);
                c = (disp - y1) * (disp - y1) - r1 * r1 + x1 * x1;
                double disc;
                disc = b * b - 4 * a * c;// 一元二次方程判别式
                if (d == Math.Abs(r1 - r2) || d == r1 + r2)
                {
                    coor = new Coordinate();
                    coor.setX(Math.Abs(((-b) / (2 * a))/20));
                    coor.setY(Math.Abs((k * coor.getX() + disp)/20));
                    points.Add(coor);
                }
                else
                {
                    coor = new Coordinate();
                    coor.setX(Math.Abs((((-b) + Math.Sqrt(disc)) / (2 * a))/20));
                    coor.setY(Math.Abs((k * coor.getX() + disp)/20));
                    points.Add(coor);
                    coor = new Coordinate();
                    coor.setX(Math.Abs((((-b) - Math.Sqrt(disc)) / (2 * a))/20));
                    coor.setY(Math.Abs((k * coor.getX() + disp)/20));
                    points.Add(coor);
                }
            }
            return points;
        }

    }
}
