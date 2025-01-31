import express from 'express';
import cors from 'cors';
import { processInvestorData } from './data';
import { Investor } from './types';

const app = express();
const port = 3000;

app.use(cors());
app.use(express.json());

// Cache the investor data
const investors: Investor[] = processInvestorData();

// GET: api/investors - returns all investors with their total commitments
app.get('/api/investors', (req, res) => {
  const investorSummaries = investors.map(({ id, name, type, country, dateAdded, lastUpdated, totalCommitment }) => ({
    id,
    name,
    type,
    country,
    dateAdded,
    lastUpdated,
    totalCommitment
  }));
  res.json(investorSummaries);
});

// GET: api/investors/:investorId/commitments - returns filtered commitments for an investor
app.get('/api/investors/:investorId/commitments', (req, res) => {
  const { investorId } = req.params;
  const { assetClass } = req.query;

  const investor = investors.find(inv => inv.id === investorId);

  if (!investor) {
    return res.status(404).json({ error: 'Investor not found' });
  }

  let commitments = investor.commitments;

  if (assetClass) {
    commitments = commitments.filter(
      commitment => commitment.assetClass === assetClass
    );
  }

  res.json(commitments);
});

app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});