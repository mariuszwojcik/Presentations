module RuleAST =

    type MetricName = string
    type Percent = decimal

    type ConstantType =
        | Percent of Percent
        | Number of decimal
    
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
