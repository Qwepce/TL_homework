import { Line } from 'react-chartjs-2';
import type { ExchangeRate } from '../../types/types';
import { useState } from 'react';
import getFormattedDate from '../../utils/FormattedDate';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  Filler
} from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend, Filler);

interface CurrencyChartProps {
  data?: ExchangeRate[];
}

const CurrencyChart = ({ data = [] }: CurrencyChartProps) => {
  const [timeRange, setTimeRange] = useState<number>(5);

  if (!data || data.length === 0) {
    return <div>Could not get data</div>;
  }

  const { purchasedCurrencyCode, paymentCurrencyCode } = data[0];

  const filterDataByTimeRange = () => {
    if (data.length === 0) {
      return [];
    }

    const now = new Date();
    const minutesAgo = new Date(now.getTime() - timeRange * 60000);

    return data.filter((item) => {
      const itemDate = new Date(item.dateTime);
      return itemDate > minutesAgo;
    });
  };

  const filteredData = filterDataByTimeRange();

  const chartData = {
    labels: filteredData.map((item) => getFormattedDate(new Date(item.dateTime))),
    datasets: [
      {
        label: `Exchange rate ${purchasedCurrencyCode}/${paymentCurrencyCode}`,
        data: filteredData.map((item) => item.price),
        borderColor: '#4165c1',
        backgroundColor: 'rgba(66, 101, 193, 0.2)',
        fill: true,
        tension: 0.1
      }
    ]
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: false
      },
      title: {
        display: false
      }
    },
    scales: {
      x: {
        ticks: {
          display: false
        },
        grid: {
          display: true
        }
      },
      y: {
        beginAtZero: false
      }
    }
  };

  return (
    <div style={{ width: '100%', maxWidth: '500px' }}>
      <div style={{ marginBottom: '5px', display: 'flex', justifyContent: 'end', gap: '45px' }}>
        {[5, 4, 3, 2, 1].map((minutes) => (
          <button
            key={minutes}
            onClick={() => {
              setTimeRange(minutes);
            }}
            style={{
              padding: '5px 10px',
              backgroundColor: timeRange === minutes ? '#4165c1' : '#eee',
              color: timeRange === minutes ? 'white' : 'black',
              border: '1px solid #ddd',
              borderRadius: '4px',
              cursor: 'pointer'
            }}
            type="button"
          >
            {`${minutes} MIN`}
          </button>
        ))}
      </div>
      {filteredData.length > 0 ? (
        <Line options={options} data={chartData} />
      ) : (
        <div>No data for selected date range</div>
      )}
    </div>
  );
};

export default CurrencyChart;