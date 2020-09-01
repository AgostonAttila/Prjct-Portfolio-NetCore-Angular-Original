export interface Fund {
  Currency: string;
  FundName: string;
  ISINNumber: string;
  Management: string;
  Focus: string;
  Type: string;
  PerformanceYTD: string;
  Performance1Year: string;
  Performance3Year: string;
  Performance5Year: string;
  PerformanceFromBeggining: string;
  PerformanceActualMinus9: string;
  PerformanceActualMinus8: string;
  PerformanceActualMinus7: string;
  PerformanceActualMinus6: string;
  PerformanceActualMinus5: string;
  PerformanceActualMinus4 :string;
  PerformanceActualMinus3: string;
  PerformanceActualMinus2: string;
  PerformanceActualMinus1: string;
  PerformanceAverage: string;

  Url: string;

  VolatilityArray: string[];
  SharpRateArray: string[];
  BestMonthArray: string[];
  WorstMonthArray: string[];
  MaxLossArray: string[];
  OverFulFilmentArray: string[];



  VolatilityString: string;
  SharpRateString: string;
  BestMonthString: string;
  WorstMonthString: string;
  MaxLossString: string;
  OverFulFilmentStringy: string;

}
