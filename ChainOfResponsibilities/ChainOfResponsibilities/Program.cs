using System;

namespace ChainOfResponsibilities
{
    class Program
    {
        static void Main(string[] args)
        {
            BudgetApprover approver1 = new BudgetApprover(new Official("Minister", 1_000_000));
            BudgetApprover approver2 = new BudgetApprover(new Official("Governor", 10000));
            BudgetApprover approver3 = new BudgetApprover(new Official("Clerck", 1000));

            approver3.RegisterNext(approver2);
            approver2.RegisterNext(approver1);

            Console.WriteLine(approver1.Approve(100));
            Console.WriteLine(approver1.Approve(10000));
            Console.WriteLine(approver1.Approve(1_000_000));
            Console.WriteLine(approver1.Approve(10_000_000));
        }

        interface IBudgetApprover
        {
            void RegisterNext(IBudgetApprover validator);
            ApprovalResponse Approve(int value);
        }

        public enum ApprovalResponse
        {
            Denied,
            Approved,
            BeyondApprovalLimit
        }

        class BudgetApprover : IBudgetApprover
        {
            private Official official;
            private IBudgetApprover nextApprover;
            public BudgetApprover(Official official)
            {
                this.official = official;
                nextApprover = new EndOfApprovalChain();
            }

            public ApprovalResponse Approve(int value)
            {
                var response = official.ApproveBudget(value);
                if (response == ApprovalResponse.BeyondApprovalLimit)
                    return nextApprover.Approve(value);
                return response;
            }

            public void RegisterNext(IBudgetApprover nextApprover)
            {
                this.nextApprover = nextApprover;
            }
        }

        class EndOfApprovalChain : IBudgetApprover
        {
            public ApprovalResponse Approve(int value)
            {
                return ApprovalResponse.Denied;
            }

            public void RegisterNext(IBudgetApprover validator)
            {
                throw new InvalidOperationException();
            }
        }

        public class Official
        {
            public string Name { get; private set; }
            private int approvalLimit;

            public Official(string name, int approvalLimit)
            {
                Name = name;
                this.approvalLimit = approvalLimit;
            }

            public ApprovalResponse ApproveBudget(int value)
            {
                return value > approvalLimit ? ApprovalResponse.BeyondApprovalLimit : ApprovalResponse.Approved;
            }
        }

    }
}
