class Formatter {
  NumberWithCommas = (x) => Math.round(x).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');

  ToTitleCase = (x) => x.toLowerCase().split(' ').map((s) => s.charAt(0).toUpperCase() + s.substring(1)).join(' ');

  FormattedDate = (date, short = true) => {
    const dt = new Date(date);
    return short
      ? dt.toLocaleDateString('us-EN', { month: 'short', day: 'numeric' })
      : dt.toLocaleDateString('us-EN', { month: 'long', day: 'numeric', year: 'numeric' });
  };

  ReplaceAllTokens = (str, obj) => {
    if (str === null) {
      return '';
    }
    let retStr = str;
    Object.keys(obj).forEach((x) => {
      retStr = retStr.replace(new RegExp(x, 'g'), obj[x]);
    });
    return retStr;
  };
}

const formatter = new Formatter();
export default formatter;
