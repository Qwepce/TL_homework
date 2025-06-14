export default function getFormattedDate(currentDate: Date): string {
  const day = currentDate.toLocaleDateString('en-US', { weekday: 'long' });
  const dateFormat = currentDate.toLocaleDateString('en-US', {
    month: 'long',
    day: 'numeric',
    year: 'numeric'
  });

  const hours = (currentDate.getUTCHours() + 3) % 24;
  const minutes = currentDate.getUTCMinutes();
  
  const formattedHours = hours.toString().padStart(2, '0');
  const formattedMinutes = minutes.toString().padStart(2, '0');

  const time = `${formattedHours}:${formattedMinutes} UTC`;

  return `${day.substring(0, 3)}, ${dateFormat}, ${time}`;
}