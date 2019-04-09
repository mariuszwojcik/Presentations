namespace TravelRepublic.Meta.BidRecommender.Compilers

module RuleAST =
    type Percent = Percent of float
    type MetricName = MetricName of string

    type ConstantType =
        | Number of decimal
        | Percent of Percent
    
    and ArithmenticOperator = 
        | Add
        | Subtract
        | Divide
        | Multiply

    and Operator =
        | Constant of ConstantType
        | Metric of MetricName
        | Formula of Operator * (ArithmenticOperator * Operator) list

    and ComparisonOperator = Equal | NotEqual | LessThan | LessOrEqual | MoreThan | MoreOrEqual

    and Condition = Operator * ComparisonOperator * Operator


// clicks = 35
// cost > margin
// margin / cost < 50%