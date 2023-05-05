import React from 'react';
import PropTypes from 'prop-types';

export default function Swatch(props) {
  const { name, hex, bg } = props;
  return (
    <div
      style={{
        display: 'flex',
        flexDirection: 'column',
        height: '175px',
        width: '150px',
        boxShadow: '0px 3px 6px #001c3b29',
        margin: '0 1rem 1rem 0',
      }}
    >
      <div
        className={bg}
        style={{
          backgroundColor: `var(${bg})`,
          height: '100px',
          width: '100%',
        }}
      />
      <div style={{ padding: '.5rem' }}>
        <p>{name}</p>
        <p>{hex}</p>
        <p>{bg}</p>
      </div>
    </div>
  );
}

Swatch.propTypes = {
  name: PropTypes.string.isRequired,
  hex: PropTypes.string.isRequired,
  bg: PropTypes.string.isRequired,
};
