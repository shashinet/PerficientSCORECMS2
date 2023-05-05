import React from 'react';
import PropTypes from 'prop-types';

// hooks
import useOutsideClick from '../../../hooks/useOutsideClicks';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function SearchButtonNav(props) {
  const { block } = props;

  const {
    ref,
    isComponentVisible,
    setIsComponentVisible,
  } = useOutsideClick(false);

  const searchValue = React.useRef(null);

  const searchAction = () => {
    const search = `${block.searchUrl}/search/?query=${searchValue.current.value}`;
    searchValue.current.value = '';
    // eslint-disable-next-line no-unused-expressions
    setIsComponentVisible(false);
    window.location.href = search;
  };

  const slideOut = () => {
    setIsComponentVisible(!isComponentVisible);
  };

  const clearSearchInput = (e) => {
    if (e.key === 'Escape') {
      searchValue.current.value = '';
    }

    if (e.key === 'Enter') {
      searchAction();
    }
  };

  React.useEffect(() => {
    if (isComponentVisible) {
      setTimeout(() => {
        searchValue.current.focus();
      }, 500);
    }
  }, [isComponentVisible]);

  React.useEffect(() => {
    if (searchValue.current) {
      searchValue.current.addEventListener('keydown', clearSearchInput);
    }
    return () => {
      searchValue.current.removeEventListener('keydown', clearSearchInput);
    };
  }, []);

  return (
    <div className={isComponentVisible ? 'search open' : 'search'} ref={ref}>
      <button
        className={isComponentVisible ? 'search-button open' : 'search-button'}
        type="button"
        onClick={slideOut}
      >
        {isComponentVisible && (
          <span className="search-caret">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="8"
              height="15"
              viewBox="0 0 8 15"
              fill="none"
            >
              <path
                d="M1 13.3669L7 7.36688L1 1.36688"
                stroke="#cc1f20"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </span>
        )}
        <span>{block.title}</span>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="21"
          height="21"
          viewBox="0 0 21 21"
          fill="none"
        >
          <path
            fillRule="evenodd"
            clipRule="evenodd"
            d="M14.1624 13.8469C15.3101 12.4621 16 10.6842 16 8.74512C16 4.32684 12.4183 0.745117 8 0.745117C3.58172 0.745117 0 4.32684 0 8.74512C0 13.1634 3.58172 16.7451 8 16.7451C9.93907 16.7451 11.717 16.0552 13.1018 14.9075L18.9234 20.7292C19.2163 21.0221 19.6912 21.0221 19.9841 20.7292C20.277 20.4363 20.277 19.9614 19.9841 19.6685L14.1624 13.8469ZM14.5 8.74512C14.5 12.335 11.5899 15.2451 8 15.2451C4.41015 15.2451 1.5 12.335 1.5 8.74512C1.5 5.15527 4.41015 2.24512 8 2.24512C11.5899 2.24512 14.5 5.15527 14.5 8.74512Z"
            fill="white"
          />
        </svg>
      </button>
      <div className={isComponentVisible ? 'search-input slide-in' : 'search-input slide-out'}>
        <input
          type="text"
          placeholder={block.placeholderText}
          onKeyUp={clearSearchInput}
          ref={searchValue}
          tabIndex={isComponentVisible ? '0' : '-1'}
          disabled={!isComponentVisible}
        />
        <button
          className="search-action"
          type="button"
          aria-label="initiate search"
          onKeyDown={searchAction}
          onClick={searchAction}
          tabIndex={isComponentVisible ? '0' : '-1'}
        >
          Search
        </button>
        <button
          className="close-action"
          type="button"
          aria-label="clear search slide out"
          onClick={() => setIsComponentVisible(!isComponentVisible)}
          tabIndex={isComponentVisible ? '0' : '-1'}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="20"
            viewBox="0 0 20 20"
            fill="none"
          >
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M1.61872 0.381282C1.27701 0.0395729 0.72299 0.0395729 0.381282 0.381282C0.0395729 0.72299 0.0395729 1.27701 0.381282 1.61872L8.76256 10L0.381282 18.3813C0.0395729 18.723 0.0395729 19.277 0.381282 19.6187C0.72299 19.9604 1.27701 19.9604 1.61872 19.6187L10 11.2374L18.3813 19.6187C18.723 19.9604 19.277 19.9604 19.6187 19.6187C19.9604 19.277 19.9604 18.723 19.6187 18.3813L11.2374 10L19.6187 1.61872C19.9604 1.27701 19.9604 0.72299 19.6187 0.381282C19.277 0.0395729 18.723 0.0395729 18.3813 0.381282L10 8.76256L1.61872 0.381282Z"
              fill="white"
            />
          </svg>
        </button>
        {block.typeaheadItems && (
          <div className="typeahead-container">
            <ul>
              <li />
            </ul>
          </div>
        )}
      </div>
    </div>
  );
}

SearchButtonNav.propTypes = {
  block: PropTypes.shape({
    title: PropTypes.string,
    placeholderText: PropTypes.string,
    typeaheadItems: PropTypes.string,
    searchUrl: PropTypes.string,
  }),
};

SearchButtonNav.defaultProps = {
  block: {},
};
