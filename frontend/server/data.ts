import { Investor, InvestorData } from './types';

const rawData: InvestorData[] = [
  {
    name: "Ioo Gryffindor fund",
    type: "fund manager",
    country: "Singapore",
    dateAdded: "2000-07-06",
    lastUpdated: "2024-02-21",
    assetClass: "Infrastructure",
    amount: 15000000,
    currency: "GBP"
  },
  {
    name: "Ibx Skywalker ltd",
    type: "asset manager",
    country: "United States",
    dateAdded: "1997-07-21",
    lastUpdated: "2024-02-21",
    assetClass: "Infrastructure",
    amount: 31000000,
    currency: "GBP"
  },
  {
    name: "Cza Weasley fund",
    type: "wealth manager",
    country: "United Kingdom",
    dateAdded: "2002-05-29",
    lastUpdated: "2024-02-21",
    assetClass: "Hedge Funds",
    amount: 58000000,
    currency: "GBP"
  },
  {
    name: "Mjd Jedi fund",
    type: "bank",
    country: "China",
    dateAdded: "2010-06-08",
    lastUpdated: "2024-02-21",
    assetClass: "Private Equity",
    amount: 72000000,
    currency: "GBP"
  },
  {
    name: "Mjd Jedi fund",
    type: "bank",
    country: "China",
    dateAdded: "2010-06-08",
    lastUpdated: "2024-02-21",
    assetClass: "Natural Resources",
    amount: 1000000,
    currency: "GBP"
  },
  {
    name: "Ibx Skywalker ltd",
    type: "asset manager",
    country: "United States",
    dateAdded: "1997-07-21",
    lastUpdated: "2024-02-21",
    assetClass: "Real Estate",
    amount: 17000000,
    currency: "GBP"
  },
  {
    name: "Ibx Skywalker ltd",
    type: "asset manager",
    country: "United States",
    dateAdded: "1997-07-21",
    lastUpdated: "2024-02-21",
    assetClass: "Real Estate",
    amount: 83000000,
    currency: "GBP"
  },
  {
    name: "Mjd Jedi fund",
    type: "bank",
    country: "China",
    dateAdded: "2010-06-08",
    lastUpdated: "2024-02-21",
    assetClass: "Hedge Funds",
    amount: 28000000,
    currency: "GBP"
  },
  {
    name: "Ibx Skywalker ltd",
    type: "asset manager",
    country: "United States",
    dateAdded: "1997-07-21",
    lastUpdated: "2024-02-21",
    assetClass: "Hedge Funds",
    amount: 85000000,
    currency: "GBP"
  }
];

export const processInvestorData = (): Investor[] => {
  const investorMap = new Map<string, Investor>();

  rawData.forEach((item) => {
    if (!investorMap.has(item.name)) {
      investorMap.set(item.name, {
        id: Buffer.from(item.name).toString('base64'),
        name: item.name,
        type: item.type,
        country: item.country,
        dateAdded: item.dateAdded,
        lastUpdated: item.lastUpdated,
        totalCommitment: 0,
        commitments: []
      });
    }

    const investor = investorMap.get(item.name)!;
    investor.commitments.push({
      assetClass: item.assetClass,
      amount: item.amount,
      currency: item.currency
    });
    investor.totalCommitment += item.amount;
  });

  return Array.from(investorMap.values());
};