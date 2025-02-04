import {currencyConfig} from '../config/currencyConfig';

const formatAmount = (amount: number): string => {
    let amountStr = amount.toLocaleString(currencyConfig.locale);
    if (isNaN(amount)) {
        amountStr = '0.00';
    }
    if (amount >= 1000000000) {
        amountStr = `${amount / 1000000000}B`;
    }else

    if (amount >= 1000000) {
        amountStr = `${amount / 1000000}M`;
    }else

    if (amount >= 1000) {
        amountStr = `${amount / 1000}K`;
    }else{
        amountStr = new Intl.NumberFormat(currencyConfig.locale, {
            style: 'currency',
            currency: 'GBP',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
          }).format(amount);
          
          return amountStr;
    }
   
    return `${currencyConfig.symbol}${amountStr}`;
};

export default formatAmount;