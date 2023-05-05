import React from 'react';
import PropTypes from 'prop-types';

const SearchFilters = ({ handleFilter, labelText, ordered, filters }) => (
  <div className="filter-type">
    <label>{labelText}</label>
    <div className="button-wrapper">
      {React.Children.toArray(filters.map((button) => (
        <button
          type="button"
          className={ordered.includes(button) ? 'active' : ''}
          aria-label={`Filter by results by ${button}`}
          tabIndex={ordered.includes(button) ? '0' : '-1'}
          onClick={(e) => handleFilter(e)}
        >
          {button}
        </button>
      )))}
    </div>
  </div>
);

SearchFilters.propTypes = {
  ordered: PropTypes.arrayOf(PropTypes.string).isRequired,
  labelText: PropTypes.string.isRequired,
  handleFilter: PropTypes.func.isRequired,
  filters: PropTypes.arrayOf(PropTypes.string).isRequired,
};

export default React.memo(SearchFilters);
