import React from 'react';
import Proptypes from 'prop-types';

export default function Table(props) {
  const { selections, tableTitle, tableHeaders, tableRowData } = props;

  return (
    <div className="outter-table">
      <table className={['table-responsive', `${selections}`].join(' ')}>
        {tableTitle && <caption>{tableTitle}</caption>}
        <thead>
          <tr>
            {tableHeaders.map((tableHeader) => (
              <th scope="col">{tableHeader}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {tableRowData.map((tableRowSingleDataArray) => (
            <tr>
              {tableRowSingleDataArray.map((tableRowSingleDataArrayItem) => (
                <td>{tableRowSingleDataArrayItem}</td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

Table.propTypes = {
  selections: Proptypes.string,
  tableTitle: Proptypes.string,
  tableHeaders: Proptypes.arrayOf(Proptypes.string),
  tableRowData: Proptypes.arrayOf(Proptypes.arrayOf()),
};

Table.defaultProps = {
  selections: 'default',
  tableTitle: null,
  tableHeaders: ['ACO Participants', 'ACO Participant in Joint Venture'],
  tableRowData: [
    ['Carolina Medicorp Enterprises, Inc.', 'No'],
    ['Forsyth Medical Group, LLC', 'No'],
    ['Forsyth Memorial Hospital, Inc.', 'No'],
  ],
};
