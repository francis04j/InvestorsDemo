export interface Commitment {
  assetClass: string;
  amount: number;
  currency: string;
}

export interface Investor {
  investorId: string;
  name: string;
  type: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  totalCommitments: number;
}