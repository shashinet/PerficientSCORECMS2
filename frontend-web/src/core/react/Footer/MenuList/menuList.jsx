/* eslint-disable jsx-a11y/no-noninteractive-element-interactions */
import React from 'react';
import PropTypes from 'prop-types';

// hooks
import useWindowSize from '../../../hooks/useWindowSize';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function MenuList(props) {
  const { block } = props;
  const [open, setOpen] = React.useState();

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      setOpen(!open);
    }
  };

  const { width } = useWindowSize();

  return (
    <>
      <ul className="menu-list">
        {width > 767 ? (
          <li className="menu-list-section" key={block.sectionTitle}>
            {block.sectionUrl ? (
              <a href={block.sectionUrl} className="menu-list-section-link">
                {block.sectionTitle}
              </a>
            ) : (
              <>{block.sectionTitle}</>
            )}
            {block.menuListContent && (
              <ul className="menu-list-links">
                {React.Children.toArray(
                  block.menuListContent.map((list) => (
                    <>
                      {list.contentType === 'RichText' ? (
                        <li
                          className="menu-list-items"
                          dangerouslySetInnerHTML={{ __html: list.value }}
                        />
                      ) : (
                        <li className="menu-list-items">
                          <a
                            href={list.url}
                            target={`${list.openInNewWindow ? '_blank' : '_self'}`}
                            aria-label={list.title}
                          >
                            {list.title}
                          </a>
                        </li>
                      )}
                    </>
                  )),
                )}
              </ul>
            )}
          </li>
        ) : (
          <li
            className={['menu-list-section', open ? 'open' : ''].join(' ')}
            onClick={() => setOpen(!open)}
            onKeyUp={(e) => handleKeyPress(e)}
            aria-label="Open Toggle sub-navigation"
            role={open ? 'menuitem' : 'button'}
          >
            {block.sectionTitle}
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="14"
              height="8"
              viewBox="0 0 14 8"
              fill="none"
            >
              <path
                d="M13 7L7 1L1 7"
                stroke="white"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
            {block.menuListContent && (
              <ul className="menu-list-links">
                {React.Children.toArray(block.menuListContent.map((list) => (
                  <>
                    {list.contentType === 'RichText' ? (
                      <li
                        className="menu-list-items"
                        dangerouslySetInnerHTML={{ __html: list.value }}
                        key={list.value}
                      />
                    ) : (
                      <li className="menu-list-items" key={list.title}>
                        <a
                          href={list.url}
                          target={`${list.openInNewWindow ? '_blank' : '_self'}`}
                          aria-label={list.title}
                        >
                          {list.title}
                        </a>
                      </li>
                    )}
                  </>
                )))}
              </ul>
            )}
          </li>
        )}
      </ul>
    </>
  );
}

MenuList.propTypes = {
  block: PropTypes.shape({
    sectionTitle: PropTypes.string,
    sectionUrl: PropTypes.string,
    menuListContent: PropTypes.arrayOf(
      PropTypes.shape({
        title: PropTypes.string,
        url: PropTypes.string,
        openInNewWindow: PropTypes.bool,
      }),
    ),
  }),
};

MenuList.defaultProps = {
  block: {
    sectionTitle: '',
    menuListContent: [
      {
        title: '',
        url: '',
        openInNewWindow: false,
      },
    ],
  },
};
