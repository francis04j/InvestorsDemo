export interface Commitment {
  assetClass: string;
  amount: number;
  currency: string;
}

export interface Investor {
  id: string;
  name: string;
  type: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  totalCommitment: number;
  commitments: Commitment[];
}

export interface InvestorData {
  name: string;
  type: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  assetClass: string;
  amount: number;
  currency: string;
}