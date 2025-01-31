import React, { useState, useEffect } from 'react';
import { InvestorList } from './components/InvestorList';
import { InvestorDetail } from './components/InvestorDetail';
import { Investor, Commitment } from './types';
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL;

if (!API_BASE_URL) {
  throw new Error('VITE_API_URL environment variable is not defined');
}

function App() {
  const [investors, setInvestors] = useState<Investor[]>([]);
  const [selectedInvestor, setSelectedInvestor] = useState<Investor | undefined>();
  const [commitments, setCommitments] = useState<Commitment[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchInvestors();
  }, []);

  useEffect(() => {
    if (selectedInvestor) {
      fetchInvestorCommitments(selectedInvestor.investorId);
    }
  }, [selectedInvestor]);

  const fetchInvestors = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/investors`);
      setInvestors(response.data);
      setLoading(false);
    } catch (err) {
      setError('Failed to fetch investors');
      setLoading(false);
    }
  };

  const fetchInvestorCommitments = async (investorId: string, assetClass?: string) => {
    try {
      const url = `${API_BASE_URL}/investors/${investorId}/commitments${
        assetClass ? `?assetClass=${assetClass}` : ''
      }`;
      const response = await axios.get(url);
      setCommitments(response.data);
    } catch (err) {
      setError('Failed to fetch investor commitments');
    }
  };

  const handleAssetClassFilter = async (assetClass: string) => {
    if (selectedInvestor) {
      await fetchInvestorCommitments(selectedInvestor.investorId, assetClass || undefined);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        <div className="text-xl text-gray-600">Loading...</div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        <div className="text-xl text-red-600">{error}</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-100">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">Investor Commitments</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <InvestorList
            investors={investors}
            onSelectInvestor={setSelectedInvestor}
            selectedInvestor={selectedInvestor}
          />
          {selectedInvestor && (
            <InvestorDetail
              investor={selectedInvestor}
              commitments={commitments}
              onAssetClassFilter={handleAssetClassFilter}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default App;