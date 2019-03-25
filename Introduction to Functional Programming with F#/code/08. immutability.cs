using System.Linq;

public class Immutablility
{
    public int SumOfNumbers()
    {
        var sum = 0;
        for (var i = 1; i < 6; i++)
        {
            sum += i;
        }

        return sum;
    }

#region Functional

    public int SumOfNumbers_functional()
    {
        var sum =
            new[] {1,2,3,4,5}
            .Aggregate(0, (acc, elem) => acc + elem);

        return sum;        
    }

#endregion

}