using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MohyoButton
{
    /// <summary>
    /// ViewModel�̊��N���X
    /// INotifyPropertyChanged �� IDataErrorInfo ����������
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region == implement of INotifyPropertyChanged ==

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if (h == null) return;
            h(this, new PropertyChangedEventArgs(propertyName));
        }

             #endregion

        #region == implemnt of IDataErrorInfo ==

        // IDataErrorInfo�p�̃G���[���b�Z�[�W��ێ����鎫��
        private Dictionary<string, string> _ErrorMessages = new Dictionary<string, string>();

        // IDataErrorInfo.Error �̎���
        string IDataErrorInfo.Error
        {
            get { return (_ErrorMessages.Count > 0) ? "Has Error" : null; }
        }

        // IDataErrorInfo.Item �̎���
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (_ErrorMessages.ContainsKey(columnName))
                    return _ErrorMessages[columnName];
                else
                    return null;
            }
        }

        // �G���[���b�Z�[�W�̃Z�b�g
        protected void SetError(string propertyName, string errorMessage)
        {
            _ErrorMessages[propertyName] = errorMessage;
        }

        // �G���[���b�Z�[�W�̃N���A
        protected void ClearErrror(string propertyName)
        {
            if (_ErrorMessages.ContainsKey(propertyName))
                _ErrorMessages.Remove(propertyName);
        }

        #endregion

    }
}