export default function getFormattedDate(currentDate: Date): string {
  const day: string = currentDate.toLocaleDateString('en-US', { weekday: 'long' });
  const dateFormat: string = currentDate.toLocaleDateString('en-US', {
    month: 'long',
    day: 'numeric',
    year: 'numeric'
  });

  const hours: number = (currentDate.getUTCHours() + 3) % 24;
  const minutes: number = currentDate.getUTCMinutes();
  
  const formattedHours: string = hours.toString().padStart(2, '0');
  const formattedMinutes: string = minutes.toString().padStart(2, '0');

  const time: string = `${formattedHours}:${formattedMinutes} UTC`;

  return `${day.substring(0, 3)}, ${dateFormat}, ${time}`;
}